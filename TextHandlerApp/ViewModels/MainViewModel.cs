using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using TextHandlerApp.Commands;
using TextHandlerApp.Interfaces;
using TextHandlerApp.Models;

namespace TextHandlerApp.ViewModels
{
    /// <summary>
    /// Класс главной модели представления
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        IDialogService dialogService; // сервис работы с диалоговыми окнами
        ITextHandler textHandlerService; // сервис работы с обработчиком файлов
        IInfoFileService infoFileService; // сервис работы с информационным файлом

        private readonly MainWindow mainWindow; // Главное окно (для передачи в параметры вызова MessageBox.Show())

        private bool success; // отчет об успешной операции обработки (не получилось сделать через Worker.CancelAsync();)

        public ObservableCollection<Document> Documents { get; set; } // Коллекция документов

        /// <summary>
        /// Выбранный документ
        /// </summary>
        private Document selectedDocument;
        public Document SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged("SelectedDocument");
            }
        }

        /// <summary>
        /// Конструктор главной модели представления
        /// </summary>
        /// <param name="MainWindow">главное окно</param>
        /// <param name="DialogService">сервис работы с диалоговыми окнами</param>
        /// <param name="FileHandlerService">сервис работы с обработчиком файлов</param>
        /// <param name="InfoFile">сервис работы с информационным файлом</param>
        public MainViewModel(MainWindow MainWindow, IDialogService DialogService, ITextHandler FileHandlerService, IInfoFileService InfoFile)
        {
            this.mainWindow = MainWindow;
            this.dialogService = DialogService;
            this.textHandlerService = FileHandlerService;
            this.infoFileService = InfoFile;

            Documents = new ObservableCollection<Document>();

            // Инициализация BackgroundWorker для реализации многопоточности
            this.Worker = new BackgroundWorker { WorkerReportsProgress = true };
            this.Worker.DoWork += Worker_DoWork;
            this.Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            // Инициализация значений по умолчанию для свойств отвечающих за параметры обработки текста
            MinWordLength = 5;
            RemovePunctuationMarks = true;
        }

        #region Команды меню

        /// <summary>
        /// Команда выхода из приложения
        /// </summary>
        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ??
                (exitCommand = new RelayCommand(obj => { mainWindow.Close(); }));
            }
        }

        /// <summary>
        /// Команда открытия файла информации
        /// </summary>
        private ICommand showInfoCommand;
        public ICommand ShowInfoCommand
        {
            get
            {
                return showInfoCommand ??
                (showInfoCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        infoFileService.GetPath(@"Views\Resources\Text\Info.txt");
                        infoFileService.ShowInfoMessage(mainWindow, infoFileService.FilePath);
                    }
                    catch (Exception ex)
                    {
                        infoFileService.ShowErrorMessage(mainWindow, ex.Message);
                    }
                }));
            }
        }

        #endregion

        #region Команды для работы с коллекцией документов

        /// <summary>
        /// Команда добавить документ в коллекцию
        /// </summary>
        private ICommand addDocumentCommand;
        public ICommand AddDocumentCommand
        {
            get
            {
                return addDocumentCommand ??
                (addDocumentCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            string name = dialogService.FileName;
                            string type = dialogService.FileType;
                            string size = dialogService.FileSize;
                            string path = dialogService.FilePath;

                            Document document = new Document(name, type, size, path);

                            if (!Documents.Contains(document))
                            {
                                Documents.Add(document);
                                SelectedDocument = document;
                            }
                            else
                                dialogService.ShowInfoMessage(mainWindow, "Document has already been added");
                        }
                    }
                    catch (Exception ex)
                    {
                        dialogService.ShowErrorMessage(mainWindow, ex.Message);
                    }
                }));
            }
        }

        /// <summary>
        /// Команда удалить документ из коллекции
        /// </summary>
        private ICommand removeDocumentCommand;
        public ICommand RemoveDocumentCommand
        {
            get
            {
                return removeDocumentCommand ??
                (removeDocumentCommand = new RelayCommand(obj =>
                {
                    Document document = obj as Document;

                    if (document != null)
                    {
                        Documents.Remove(document);
                    }
                }, (obj) => Documents.Count > 0));
            }
        }

        /// <summary>
        /// Команда обработать документы(документ)
        /// </summary>
        private ICommand proceedCommand;
        public ICommand ProceedCommand
        {
            get
            {
                return proceedCommand ??
                (proceedCommand = new RelayCommand(obj =>
                {
                    if (dialogService.SelectFolderDialog() == true)
                    {
                        success = true;

                        // Обработка документа(документов) в отдельном потоке
                        Worker.RunWorkerAsync();
                    }

                }, (obj) => Documents.Count > 0));
            }
        }

        #endregion

        #region Многопоточность

        public BackgroundWorker Worker { get; private set; }

        /// <summary>
        /// Метод обрабатывающий документы в дополнительном потоке
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="e">аргументы</param>
        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DocumentsVisibility = false; // Сокрытие коллекции документов
            ProgressBarVisibility = true; // Отображение прогресс бара

            try
            {
                // Обработка документа(документов)
                textHandlerService.HandleDocuments(Documents.ToList(), dialogService.FolderPath, new Options(RemovePunctuationMarks, MinWordLength));
            }
            catch (Exception ex)
            {
                success = false;

                //Используем Dispatcher для выполнения метода в потоке UI
                mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate
                    {
                        Documents.Clear();
                        dialogService.ShowErrorMessage(mainWindow, ex.Message);
                    });
            }
        }

        /// <summary>
        /// Метод срабатывабщий по завершению обработки в дополнительном потоке
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="e">аргументы</param>
        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarVisibility = false; // Сокрытие прогресс бара
            DocumentsVisibility = true; // Отображение коллекции документов

            if (success)
            {
                // Вызов сообщения об окончании обработки документа(документов)
                dialogService.ShowInfoMessage(mainWindow, "Document is handled");
            }
        }

        #endregion

        #region Отображение UI элементов

        /// <summary>
        /// Свойство отвечающее за отображение коллекции документов
        /// </summary>
        private bool documentsVisibility = true;
        public bool DocumentsVisibility
        {
            get { return documentsVisibility; }
            set
            {
                documentsVisibility = value;
                OnPropertyChanged("DocumentsVisibility");
            }
        }

        /// <summary>
        /// Свойство отвечающее за отображение прогресс бара
        /// </summary>
        private bool progressBarVisibility;
        public bool ProgressBarVisibility
        {
            get { return progressBarVisibility; }
            set
            {
                progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        #endregion

        #region Настройки обработки текста

        /// <summary>
        /// Свойство минимально допустимого количества символов в слове
        /// </summary>
        private int minWordLength;
        public int MinWordLength
        {
            get { return minWordLength; }
            set
            {
                minWordLength = value;
                OnPropertyChanged("MinWordLength");
            }
        }

        /// <summary>
        /// Свойство удаления пунктуации
        /// </summary>
        private bool removePunctuationMarks;
        public bool RemovePunctuationMarks
        {
            get { return removePunctuationMarks; }
            set
            {
                removePunctuationMarks = value;
                OnPropertyChanged("RemovePunctuationMarks");
            }
        }

        #endregion
    }
}

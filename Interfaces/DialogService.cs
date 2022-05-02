﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanator.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        bool ShowMessageOKCancel(string message);
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
}

/*
 * Copyright (C) 2019 Medusalix
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Picross.Model;

namespace Picross.ViewModel
{
    /// <summary>
    /// Main view model for the game
    /// </summary>
    class ViewModel
    {
        /// <summary>
        /// Window title with version information
        /// </summary>
        public string Title { get; } = string.Format(
            Properties.Resources.Title,
            Assembly.GetExecutingAssembly().GetName().Version.ToString(3)
        );

        public Settings Settings { get; }
        public Grid Grid { get; }

        public ICommand NewGameDialogCommand { get; }

        public ICommand NewGameCommand { get; }
        public ICommand GridChangedCommand { get; }

        public ViewModel()
        {
            Settings = new Settings();
            Grid = new Grid(Settings);

            NewGameDialogCommand = new RelayCommand(OpenNewGameDialog);
            NewGameCommand = new RelayCommand(p => ((Window)p).DialogResult = true);
            GridChangedCommand = new RelayCommand(GridChanged);

            Grid.Shuffle();
        }

        private void GridChanged(object parameter)
        {
            Grid.Update((Item)parameter);

            if (Grid.Solved)
            {
                MessageBox.Show(
                    Properties.Resources.SolvedText,
                    Properties.Resources.SolvedTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                Grid.Shuffle();
            }
        }

        private void OpenNewGameDialog()
        {
            NewGameDialog dialog = new NewGameDialog
            {
                DataContext = this
            };

            if (dialog.ShowDialog() == true)
            {
                Grid.Shuffle();
            }
        }
    }
}

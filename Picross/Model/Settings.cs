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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Picross.Model
{
    /// <summary>
    /// Stores the settings of the game
    /// </summary>
    class Settings : INotifyPropertyChanged
    {
        /// <summary>
        /// Number of rows
        /// </summary>
        public int Rows
        {
            get => rows;
            set
            {
                rows = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Number of columns
        /// </summary>
        public int Columns
        {
            get => columns;
            set
            {
                columns = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Difficulty from 0 (easy) to 3 (extreme)
        /// </summary>
        public int Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Chance of a cross for each difficulty from 0 to 100%
        /// </summary>
        private static readonly int[] CrossProbabilities = { 60, 50, 40, 20 };

        /// <summary>
        /// Default settings
        /// </summary>
        private int rows = 8;
        private int columns = 8;
        private int difficulty = 1;
        
        /// <summary>
        /// Probability of a cross based on the difficulty
        /// </summary>
        /// <returns>Probability of a cross</returns>
        public int GetCrossProbability()
        {
            return CrossProbabilities[Difficulty];
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

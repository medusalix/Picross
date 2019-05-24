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
    /// Represents a single box in the grid
    /// </summary>
    class Item : INotifyPropertyChanged
    {
        public int Row { get; }
        public int Column { get; }

        /// <summary>
        /// Current crossed status
        /// true = Box is crossed
        /// false = Box is empty
        /// null = Box can't be crossed
        /// </summary>
        public bool? Crossed
        {
            get => crossed;
            set
            {
                crossed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Puzzle solution
        /// true = Box is crossed
        /// false = Box is empty
        /// </summary>
        public bool Solution { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool? crossed = false;

        public Item(int row, int column, bool solution)
        {
            Row = row;
            Column = column;
            Solution = solution;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

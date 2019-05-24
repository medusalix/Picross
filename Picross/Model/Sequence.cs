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
    /// Stores information about a certain row/column
    /// </summary>
    class Sequence : INotifyPropertyChanged
    {
        /// <summary>
        /// Hints displayed next to a row/column
        /// </summary>
        public List<int> NumberHints { get; }

        public bool Solved
        {
            get => solved;
            set
            {
                solved = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool solved;

        public Sequence(IList<Item> items)
        {
            NumberHints = CountHints(items, true)
                .ToList();

            // No crosses in the sequence
            if (NumberHints.Count == 0)
            {
                Solved = true;
            }
        }

        /// <summary>
        /// Check if a specific row/column is solved
        /// </summary>
        /// <param name="crosses">Actual crosses to compare to</param>
        public void Update(IList<Item> crosses)
        {
            Solved = NumberHints.SequenceEqual(CountHints(crosses, false));
        }

        private IEnumerable<int> CountHints(IList<Item> items, bool solution)
        {
            int count = 0;

            foreach (Item item in items)
            {
                // Either count crosses or solutions
                bool crossed = solution
                    ? item.Solution
                    : item.Crossed == true;

                if (crossed)
                {
                    count++;
                }

                else if (count > 0)
                {
                    yield return count;

                    count = 0;
                }
            }

            if (count > 0)
            {
                yield return count;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

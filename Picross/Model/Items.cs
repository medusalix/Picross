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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picross.Model
{
    /// <summary>
    /// Wrapper for the actual grid
    /// </summary>
    class Items : ObservableCollection<IList<Item>>
    {
        /// <summary>
        /// Gets a single row of the grid
        /// </summary>
        /// <param name="row">Row index</param>
        /// <returns>Row at index</returns>
        public IList<Item> Row(int row)
        {
            return this[row];
        }

        /// <summary>
        /// Gets a single column of the grid
        /// </summary>
        /// <param name="column">Column index</param>
        /// <returns>Column at index</returns>
        public IList<Item> Column(int column)
        {
            return Enumerable.Range(0, this[0].Count)
                .Select(row => this[row][column])
                .ToList();
        }
    }
}

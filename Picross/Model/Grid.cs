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
    /// Main puzzle model
    /// </summary>
    class Grid
    {
        /// <summary>
        /// All items (boxes) in the grid
        /// </summary>
        public Items Items { get; } = new Items();

        /// <summary>
        /// Horizontal rows with hints
        /// </summary>
        public IList<Sequence> HorizontalSequences { get; } = new ObservableCollection<Sequence>();

        /// <summary>
        /// Vertical columns with hints
        /// </summary>
        public IList<Sequence> VerticalSequences { get; } = new ObservableCollection<Sequence>();

        /// <summary>
        /// Player solved the puzzle
        /// </summary>
        public bool Solved { get; private set; }

        private Settings settings;
        private Random random = new Random();

        public Grid(Settings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Randomize/reset all crosses
        /// </summary>
        public void Shuffle()
        {
            Items.Clear();
            HorizontalSequences.Clear();
            VerticalSequences.Clear();

            int probability = settings.GetCrossProbability();

            for (int i = 0; i < settings.Rows; i++)
            {
                IList<Item> row = new List<Item>();

                for (int j = 0; j < settings.Columns; j++)
                {
                    bool crossed = random.Next(101) < probability;

                    row.Add(new Item(i, j, crossed));
                }

                Items.Add(row);
                HorizontalSequences.Add(new Sequence(row));
            }

            for (int i = 0; i < settings.Columns; i++)
            {
                VerticalSequences.Add(new Sequence(Items.Column(i)));
            }
        }

        /// <summary>
        /// Update the changed row/column and solved status
        /// </summary>
        /// <param name="item">Item/cross that changed</param>
        public void Update(Item item)
        {
            HorizontalSequences[item.Row].Update(Items.Row(item.Row));
            VerticalSequences[item.Column].Update(Items.Column(item.Column));

            Solved = HorizontalSequences.Union(VerticalSequences)
                .All(group => group.Solved);
        }
    }
}

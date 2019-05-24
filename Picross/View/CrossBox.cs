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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Picross.View
{
    /// <summary>
    /// Visual representation of a box in the grid
    /// </summary>
    class CrossBox : CheckBox
    {
        static CrossBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CrossBox),
                new FrameworkPropertyMetadata(typeof(CrossBox))
            );
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            OnRightClick();
        }

        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            e.MouseDevice.Capture(null);

            if (IsChecked != null)
            {
                OnClick();
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (e.LeftButton == MouseButtonState.Pressed && IsChecked != null)
            {
                OnClick();
            }

            else if (e.RightButton == MouseButtonState.Pressed && IsChecked == false)
            {
                OnRightClick();
            }
        }

        private void OnRightClick()
        {
            IsChecked = IsChecked == null ? false : default(bool?);

            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}

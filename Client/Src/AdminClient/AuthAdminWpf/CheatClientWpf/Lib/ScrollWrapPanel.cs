using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CheatClientWpf
{
    public class ScrollWrapPanel : WrapPanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double maxChildWidth = 0;
            if (Children.Count > 0)
            {
                foreach (UIElement el in Children)
                {
                    if (el.DesiredSize.Width > maxChildWidth)
                    {
                        maxChildWidth = el.DesiredSize.Width;
                    }
                }
            }
            MinWidth = maxChildWidth;
            return base.MeasureOverride(availableSize);
        }
    }
}

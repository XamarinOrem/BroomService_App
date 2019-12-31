using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BroomService_App.CustomControls
{
    public class SpacingModifier
    {
        public SpacingModifier(CollectionView CV)
        {
            try
            {
                if (CV.ItemsLayout is GridItemsLayout gridItemsLayout)
                {
                    gridItemsLayout.VerticalItemSpacing = 10.0;
                    gridItemsLayout.HorizontalItemSpacing = 10.0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception_:-" + ex.Message);
            }
        }
    }
}

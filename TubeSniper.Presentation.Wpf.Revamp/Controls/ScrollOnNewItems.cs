﻿using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace TubeSniper.Presentation.Wpf.Controls
{
	public class AutoScrollToLastItemBehavior : Behavior<ListBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
			if (collection != null)
				collection.CollectionChanged += collection_CollectionChanged;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
			if (collection != null)
				collection.CollectionChanged -= collection_CollectionChanged;
		}

		private void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				ScrollToLastItem();
			}
		}

		private void ScrollToLastItem()
		{
			int count = AssociatedObject.Items.Count;
			if (count > 0)
			{
				var last = AssociatedObject.Items[count - 1];
				AssociatedObject.ScrollIntoView(last);
			}
		}
	}
}
namespace IFiscalV2.Models
{
    using IFiscalV2.Common;
    using System;

    public class MenuItemModel
    {
        public MenuItemType Id { get; set; }

        public string MenuTitle { get; set; }

        public string MenuIcon { get; set; }

        public bool IsDetail { get; set; } = true;
        public bool CachedPage { get; set; } = false;

        public Type TargetType { get; set; }

    }
}

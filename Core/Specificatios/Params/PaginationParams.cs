﻿namespace Core.Specificatios.Params
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value;
        }
    }
}

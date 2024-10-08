﻿using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IBookService
    {
        //void AddBook(string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate);
        //void UpdateBook(int id, string? title, string? author, string? inventoryNumber, short? year, DateOnly? receiptDate, DateOnly? writeOffDate);
        bool DeleteBook(int id);
        bool WriteOFFBook(int id, DateOnly date);
        void AddBookObj(Book book);
        bool UpdateBookObj(int id, Book book);      
        string GetBook(int id);
    }
}

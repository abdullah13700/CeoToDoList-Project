using CeoToDoList.Models.Domain;
using System;
using System.Collections.Generic;

namespace UnitTest.Mocking
{
    public static class MockedLists
    {
        public static List<CeoList> GetMockedLists()
        {
            return new List<CeoList>
            {
                new CeoList
                {
                    Id = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7"),
                    Title = "List 1",
                    Description = "Description for List 1"
                },
                new CeoList
                {
                    Id = Guid.NewGuid(),
                    Title = "List 2",
                    Description = "Description for List 2"
                },
                new CeoList
                {
                    Id = Guid.NewGuid(),
                    Title = "List 3",
                    Description = "Description for List 3"
                }
            };
        }
    }
}

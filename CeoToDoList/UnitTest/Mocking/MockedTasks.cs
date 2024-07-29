using CeoToDoList.Models.Domain;
using System;
using System.Collections.Generic;

namespace UnitTest.Mocking
{
    public static class MockedTasks
    {
        public static List<CeoTask> GetMockedTasks()
        {
            return new List<CeoTask>
            {
                new CeoTask
                {
                    Id = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d0"),
                    Title = "Duplicated Title",
                    Description = "Description for Task 1",
                    Completed = false,
                    ListId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7")
                },
                new CeoTask
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    Completed = false,
                    ListId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7")
                },
                new CeoTask
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 3",
                    Description = "Description for Task 3",
                    Completed = false,
                    ListId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7")
                }
            };
        }
    }
}

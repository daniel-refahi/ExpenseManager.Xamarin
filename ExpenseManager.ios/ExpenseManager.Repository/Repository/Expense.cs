using System;
namespace ExpenseManager.Repository
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public Category Category { get; set; }

        public void Create()
        {
            // save it to database
        }

        public void Update()
        {
            // save it to database
        }

        public void Delete()
        {
            // delete from database
        }
    }
}

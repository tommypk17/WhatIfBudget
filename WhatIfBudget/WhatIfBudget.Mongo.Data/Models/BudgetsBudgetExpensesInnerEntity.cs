using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/**
* Generated by MongoDB Relational Migrator 
* https://www.mongodb.com/products/relational-migrator 
* Collection: budgets
* Language: C#
* Template: POCO
* Generated on 3/27/24

*/
namespace WhatIfBudget.Mongo.Data.Models {

  /// <summary>
  /// 
  /// </summary>
  public class BudgetsBudgetExpensesInnerEntity {
    /// <summary>
    /// Gets or Sets Expense
    /// </summary>
    [BsonElement("expense")]
    public BudgetsBudgetExpensesInnerExpenseEntity Expense { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BudgetsBudgetExpensesInnerEntity {\n");
      sb.Append("  Expense: ").Append(Expense).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return this.ToJson<BudgetsBudgetExpensesInnerEntity>();
    }
  }
}

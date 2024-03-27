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
  public class BudgetsDebtGoalDebtsInnerDebtEntity {
    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets CurrentBalance
    /// </summary>
    [BsonElement("currentBalance")]
    public double CurrentBalance { get; set; }

    /// <summary>
    /// Gets or Sets InterestRate
    /// </summary>
    [BsonElement("interestRate")]
    public double InterestRate { get; set; }

    /// <summary>
    /// Gets or Sets MinimumPayment
    /// </summary>
    [BsonElement("minimumPayment")]
    public double MinimumPayment { get; set; }

    /// <summary>
    /// Gets or Sets CreatedOn
    /// </summary>
    [BsonElement("createdOn")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or Sets UpdatedOn
    /// </summary>
    [BsonElement("updatedOn")]
    public DateTime UpdatedOn { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BudgetsDebtGoalDebtsInnerDebtEntity {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  CurrentBalance: ").Append(CurrentBalance).Append("\n");
      sb.Append("  InterestRate: ").Append(InterestRate).Append("\n");
      sb.Append("  MinimumPayment: ").Append(MinimumPayment).Append("\n");
      sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
      sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return this.ToJson<BudgetsDebtGoalDebtsInnerDebtEntity>();
    }
  }
}
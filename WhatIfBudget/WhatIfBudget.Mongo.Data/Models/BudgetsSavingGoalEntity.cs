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
  public class BudgetsSavingGoalEntity {
    /// <summary>
    /// Gets or Sets TargetBalance
    /// </summary>
    [BsonElement("targetBalance")]
    public double TargetBalance { get; set; }

    /// <summary>
    /// Gets or Sets AnnualReturnRatePercent
    /// </summary>
    [BsonElement("annualReturnRatePercent")]
    public double AnnualReturnRatePercent { get; set; }

    /// <summary>
    /// Gets or Sets UpdatedOn
    /// </summary>
    [BsonElement("updatedOn")]
    public DateTime UpdatedOn { get; set; }

    /// <summary>
    /// Gets or Sets AdditionalBudgetAllocation
    /// </summary>
    [BsonElement("additionalBudgetAllocation")]
    public double AdditionalBudgetAllocation { get; set; }

    /// <summary>
    /// Gets or Sets CurrentBalance
    /// </summary>
    [BsonElement("currentBalance")]
    public double CurrentBalance { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class BudgetsSavingGoalEntity {\n");
      sb.Append("  TargetBalance: ").Append(TargetBalance).Append("\n");
      sb.Append("  AnnualReturnRatePercent: ").Append(AnnualReturnRatePercent).Append("\n");
      sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
      sb.Append("  AdditionalBudgetAllocation: ").Append(AdditionalBudgetAllocation).Append("\n");
      sb.Append("  CurrentBalance: ").Append(CurrentBalance).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return this.ToJson<BudgetsSavingGoalEntity>();
    }
  }
}

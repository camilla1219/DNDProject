using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebAPI.Models;
public class SurveyContext: DbContext{
    public DbSet<SurveyModel> SurveyModels => Set <SurveyModel> ();
    public DbSet<QuestionModel> QuestionModels => Set <QuestionModel> ();
    public DbSet<OptionModel> OptionModels => Set <OptionModel> ();

    //public DbSet<Respondent> Respondents => Set <Respondent> ();
    public DbSet<ResponseModel> ResponseModels => Set <ResponseModel> ();

    public string DbPath { get; }

    public SurveyContext(){

    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    DbPath = System.IO.Path.Join(path, "survey.db");
    }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source = {DbPath}");
    
}
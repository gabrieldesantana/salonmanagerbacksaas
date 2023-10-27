using SalonManager.Domain.Entities;
using SalonManager.Domain.Enums;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;
using System.Reflection.Emit;

namespace SalonManager.Service.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _repository;
    public GoalService(IGoalRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Goal>> GetAllAsync()
    {
        var goals = await _repository.GetAllAsync();

        if (goals is null) return new List<Goal>();

        return goals;
    }

    public async Task<Goal> GetByIdAsync(int id)
    {
        if (id == 999)
        {
            //for(var i = 0; i< 11; i++)
            //{
                var c = new Goal 
                {
                    Title= "Meus Ganhos",
                    CounterLabel= "Quantidade de atendimentos",
                    CounterValue = 10,
                    Labels= "Semana 1; Semana 2; Semana 3; Semana 4",
                    LabelCurrentValues= "Ganhos Semanais Atuais;",
                    CurrentValues= "50; 100; 150; 200",
                    LabelFutureValues= "Meta de Ganhos Semanais",
                    FutureValues= "100; 200; 300; 400;",
                    GoalType = Domain.Enums.EGoalType.Financeira,
                    GraphicType = "bar",
                    StartDate= DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30)

                };

                await _repository.InsertAsync(c);
            //}
        }
        var goal = await _repository.GetByIdAsync(id);

        if (goal is not null) return goal;

        return null;
    }

    public async Task<Goal> InsertAsync(InputGoalModel inputModel)
    {
        var newGoal = new Goal
        {
            Title = inputModel.Title,
            CounterLabel = inputModel.CounterLabel,
            CounterValue = inputModel.CounterValue,
            Labels = inputModel.Labels,
            LabelCurrentValues = inputModel.LabelCurrentValues,
            CurrentValues = inputModel.CurrentValues,
            LabelFutureValues = inputModel.LabelFutureValues,
            FutureValues = inputModel.FutureValues,
            GoalType = inputModel.GoalType,
            GraphicType = inputModel.GraphicType,
            StartDate = inputModel.StartDate,
            EndDate= inputModel.EndDate
        };

        return await _repository.InsertAsync(newGoal);
    }

    public async Task<Goal> UpdateAsync(int id, EditGoalModel editModel)
    {
        var goalEdit = await _repository.GetByIdAsync(id);

        if (goalEdit is null) return null;

        goalEdit.Labels = editModel.Labels;
        goalEdit.LabelCurrentValues = editModel.LabelCurrentValues;
        goalEdit.CurrentValues = editModel.CurrentValues;
        goalEdit.LabelFutureValues = editModel.LabelFutureValues;
        goalEdit.FutureValues = editModel.FutureValues;
        goalEdit.GoalType = editModel.GoalType;
        goalEdit.GraphicType = editModel.GraphicType;
        goalEdit.StartDate = editModel.StartDate;
        goalEdit.EndDate = editModel.EndDate;

        goalEdit = await _repository.UpdateAsync(goalEdit);

        return goalEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var goal = await _repository.GetByIdAsync(id);
        if (goal is null) return false;

        return await _repository.DeleteAsync(goal.Id);
    }
}

using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;

namespace Processor.Models
{
  public class BEBase : INotifyPropertyChanged
  {
    public BEBase()
    {
      _Validator = GetValidator();
      Validate();
    }

    protected IValidator _Validator = null;
    protected event PropertyChangedEventHandler _propertyChanged;
    protected IEnumerable<ValidationFailure> _validationErrors = null;
    private List<PropertyChangedEventHandler> _propertyChangedSubscribers = new List<PropertyChangedEventHandler>();

    [NotMapped]
    public bool IsValid
    {
      get
      {
        if (_validationErrors != null && _validationErrors.Count() > 0)
          return false;
        else
          return true;
      }

    }


    [NotMapped]
    public List<string> Errors = new List<string>();


    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        if (!_propertyChangedSubscribers.Contains(value))
        {
          _propertyChanged += value;
          _propertyChangedSubscribers.Add(value);
        }

      }
      remove
      {
        _propertyChanged -= value;
        _propertyChangedSubscribers.Remove(value);
      }
    }

    public void Validate()
    {
      if (_Validator != null)
      {
        Errors = new List<string>();
        ValidationResult results = _Validator.Validate(this);
        _validationErrors = results.Errors;

        foreach (var error in _validationErrors)
        {
          Errors.Add(error.ErrorMessage);
        }
      }
    }

    protected virtual IValidator GetValidator()
    {
      return null;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (_propertyChanged != null)
        _propertyChanged(this, new PropertyChangedEventArgs(propertyName));

      Validate();
    }

    protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
      string propertyName = ExtractPropertyName(propertyExpression);
      OnPropertyChanged(propertyName);
    }

    private string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
      var expression = (MemberExpression)propertyExpression.Body;

      return expression.Member.Name;
    }    
  }
}

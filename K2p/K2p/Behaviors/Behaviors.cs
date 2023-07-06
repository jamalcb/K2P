using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using K2p.Statics;
using SkiaSharp.Views.Forms;

namespace K2p.Behaviors
{
  public class IpAddressValidationBehavior : Behavior<Entry>
  {
    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      base.OnDetachingFrom(entry);
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var isValid = IsIPv4(args.NewTextValue, out var _);
      var color = ((Entry) sender).TextColor;
      ((Entry)sender).TextColor = isValid ? color : Color.Red; // Warning, will override
    }

    private bool IsIPv4(string value, out string result)
    {   if(value == null)
        {
            result = "";
            return false;
        }

      var count = value.Count(f => f == '.');
      //Debug.WriteLine(count);

      var isNumber = ulong.TryParse(value.Replace(".", ""), out var _);

      if (count == 3 && isNumber)
      {
        result = value;
        return true;
      }
      result = "";
      return false;
    }
  }
  public class FaderValidationBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid)
      {
        _temp = _temp > Constants.TrimMax ? Constants.TrimMax : _temp;
        _temp = _temp < Constants.TrimMin ? Constants.TrimMin : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);
     // var color = ((Entry)sender).TextColor;
      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class BalanceValidationBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > 0 ? 0 : _temp;
        _temp = _temp < Constants.FaderGainMin ? Constants.FaderGainMin : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var isValid = double.TryParse(args.NewTextValue, out var _temp);
     // var color = ((Entry)sender).TextColor;
      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class CenterValidationBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > Constants.FaderCenterMax ? Constants.FaderCenterMax : _temp;
        _temp = _temp < Constants.FaderCenterMin ? Constants.FaderCenterMin : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);
    //  var color = ((Entry)sender).TextColor;
      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class BassValidationBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > Constants.Fader_Bass_Max ? Constants.Fader_Bass_Max : _temp;
        _temp = _temp < Constants.Fader_Bass_Min ? Constants.Fader_Bass_Min : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);

      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class BoostCutBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > Constants.Boost_Max ? Constants.Boost_Max : _temp;
        _temp = _temp < Constants.Boost_Min ? Constants.Boost_Min : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);

      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class ParaBandBehavior : Behavior<Entry>
  {
    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }
    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }
    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > 31 ? 31 : _temp;
        _temp = _temp < 1 ? 1 : _temp;
        entry.Text = $"{_temp:N0}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);
      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class PasswordBehavior : Behavior<Entry>
  {
    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }
    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }
    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var text = entry.Text.Replace(",", string.Empty);
      var isValid = int.TryParse(text, out var _temp);
      if (isValid) 
      {
        _temp = _temp > 9999 ? 9999 : _temp;
        _temp = _temp < 0 ? 0 : _temp;
        entry.Text = $"{_temp:N0}";
      }
    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var text = args.NewTextValue.Replace(",", string.Empty);
      var isValid = int.TryParse(text, out var _temp);
      if (_temp < 0) isValid = false;
      var normalColor = ((Entry)sender).TextColor;
      ((Entry)sender).TextColor = isValid ? normalColor : Color.Red;
    }
  }
  public class ParaBoostCutBehavior : Behavior<Entry>
  {
    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > Constants.ParaMetricBoostMax ? Constants.ParaMetricBoostMax : _temp;
        _temp = _temp < Constants.ParaMetricBoostMin ? Constants.ParaMetricBoostMin : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);

      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class ParaQBehavior : Behavior<Entry>
  {
    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }
    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) // Really don't need this validation because it's coming from a slider.
      {
        _temp = _temp > Constants.ParaQMax ? Constants.ParaQMax : _temp;
        _temp = _temp < Constants.ParaQMin ? Constants.ParaQMin : _temp;
        entry.Text = $"{_temp:N1}";
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);
     // var normalColor = ((Entry)sender).TextColor;
      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }
  public class ParaFrequencyBehavior : Behavior<Entry>
  {

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      entry.Completed += OnEntryCompeted;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      entry.Completed -= OnEntryCompeted;
      base.OnDetachingFrom(entry);
    }


    private void OnEntryCompeted(object sender, EventArgs e)
    {
      var entry = (Entry)sender;
      var isValid = double.TryParse(entry.Text, out var _temp);
      if (isValid) 
      {
        _temp = _temp > Constants.ParaFreqMax ? Constants.ParaFreqMax : _temp;
        _temp = _temp < Constants.ParaFreqMin ? Constants.ParaFreqMin : _temp;
        if(_temp < 100)
        entry.Text = $"{_temp:N2}";
        if (_temp < 1000)
          entry.Text = $"{_temp:N1}";
        else
        {
          entry.Text = $"{_temp:N0}";
        }
      }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      //var entry = (Entry)sender;
      var isValid = double.TryParse(args.NewTextValue, out var _temp);

      ((Entry)sender).TextColor = isValid ? Color.White : Color.Red;
    }
  }

  public class PaintSurfaceCommandBehavior : Behavior<SKCanvasView>
  {
    // we need a bindable property for the command
    public static readonly BindableProperty CommandProperty =
      BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(PaintSurfaceCommandBehavior),
        null);

    // the command property
    public ICommand Command
    {
      get => (ICommand)GetValue(CommandProperty);
      set => SetValue(CommandProperty, value);
    }

    // invoked immediately after the behavior is attached to a control
    protected override void OnAttachedTo(SKCanvasView bindable)
    {
      base.OnAttachedTo(bindable);

      // we want to be notified when the view's context changes
      bindable.BindingContextChanged += OnBindingContextChanged;
      // we are interested in the paint event
      bindable.PaintSurface += OnPaintSurface;
    }

    // invoked when the behavior is removed from the control
    protected override void OnDetachingFrom(SKCanvasView bindable)
    {
      base.OnDetachingFrom(bindable);

      // unsubscribe from all events
      bindable.BindingContextChanged -= OnBindingContextChanged;
      bindable.PaintSurface -= OnPaintSurface;
    }

    // the view's context changed
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      // update the behavior's context to match the view
      BindingContext = ((BindableObject)sender).BindingContext;
    }

    // the canvas needs to be painted
    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
      // first check if the command can/should be fired
      if (Command?.CanExecute(e) == true)
      {
        // fire the command
        Command.Execute(e);
      }
    }
  }
  public class PaintSurfaceTouchCommandBehavior : Behavior<SKCanvasView>
  {
    // we need a bindable property for the command
    public static readonly BindableProperty CommandProperty =
      BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(PaintSurfaceTouchCommandBehavior),
        null);

    // the command property
    public ICommand Command
    {
      get => (ICommand)GetValue(CommandProperty);
      set => SetValue(CommandProperty, value);
    }

    // invoked immediately after the behavior is attached to a control
    protected override void OnAttachedTo(SKCanvasView bindable)
    {
      base.OnAttachedTo(bindable);

      // we want to be notified when the view's context changes
      bindable.BindingContextChanged += OnBindingContextChanged;
      // we are interested in the paint event
      bindable.Touch += OnTouch;
    }

    // invoked when the behavior is removed from the control
    protected override void OnDetachingFrom(SKCanvasView bindable)
    {
      base.OnDetachingFrom(bindable);

      // unsubscribe from all events
      bindable.BindingContextChanged -= OnBindingContextChanged;
      bindable.Touch -= OnTouch;
    }

    // the view's context changed
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      // update the behavior's context to match the view
      BindingContext = ((BindableObject)sender).BindingContext;
    }

    // the canvas 
    private void OnTouch(object sender, SKTouchEventArgs e)
    {
      // first check if the command can/should be fired
      if (Command?.CanExecute(e) == true)
      {
        // fire the command
        Command.Execute(e);
      }
    }
  }


}

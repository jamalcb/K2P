using System;
using System.Collections.Generic;
using System.Text;

namespace K2p.Statics
{
  public class Group
  {
    public string Name { get; set; }
    public int GroupIndex { get; set; }
  }
  

  public static class GroupData
  {
    public static IList<Group> GroupSource { get;  }

    static GroupData()
    {
      GroupSource = new List<Group>();
      for (var channel = 0; channel != 8; channel++)
      {
        GroupSource.Add(new Group
        {
          Name = $"Group {channel + 1}",
          GroupIndex = channel
        });
      }
    }
  }

  public class Assignment
  {
    public string Name { get; set; }
    public int AssignmentIndex { get; set; }
  }

  public  class AssignmentData
  {
    public  IList<Assignment> AssignmentSource { get;  }

    public AssignmentData()
    {
      AssignmentSource = new List<Assignment>
      {
        new Assignment
        {
          Name = "Front Left",
          AssignmentIndex = 0
        },
        new Assignment
        {
          Name = "Front Right",
          AssignmentIndex = 1
        },
        new Assignment
        {
          Name = "Rear Left",
          AssignmentIndex = 2
        },
        new Assignment
        {
          Name = "Rear Right",
          AssignmentIndex = 3
        },
        new Assignment
        {
          Name = "Center",
          AssignmentIndex = 4
        },
        new Assignment
        {
          Name = "Bass",
          AssignmentIndex = 5
        }
      };



    }
  }
}

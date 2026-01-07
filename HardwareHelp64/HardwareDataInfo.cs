using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HardwareHelp
{
    public class HardwareDataInfo
    {
      private List<HardwareDataInfo.DataItem> systemRunData = new List<HardwareDataInfo.DataItem>();
      private List<HardwareDataInfo.DataItem> systemData = new List<HardwareDataInfo.DataItem>();

      public void SetSystemData(List<HardwareDataInfo.DataItem> data)
      {
        try
        {
          lock (this.systemData)
          {
            this.systemData.Clear();
            foreach (HardwareDataInfo.DataItem dataItem in data)
              this.systemData.Add(dataItem);
          }
        }
        catch
        {
        }
      }

      public void SetSystemRunData(List<HardwareDataInfo.DataItem> data)
      {
        try
        {
          lock (this.systemRunData)
          {
            this.systemRunData.Clear();
            foreach (HardwareDataInfo.DataItem dataItem in data)
              this.systemRunData.Add(dataItem);
          }
        }
        catch
        {
        }
      }

      public void AddSystemData(HardwareDataInfo.DataItem item)
      {
        try
        {
          lock (this.systemData)
          {
            List<HardwareDataInfo.DataItem> systemData = this.systemData;
            for (int index = 0; index < systemData.Count; ++index)
            {
              if (systemData[index].DataType == item.DataType)
              {
                systemData[index] = item;
                item = (HardwareDataInfo.DataItem) null;
                break;
              }
            }
            if (item == null)
              return;
            systemData.Add(item);
          }
        }
        catch (Exception ex)
        {
        }
      }

      public void AddSystemRunData(HardwareDataInfo.DataItem item)
      {
        try
        {
          lock (this.systemRunData)
          {
            List<HardwareDataInfo.DataItem> systemRunData = this.systemRunData;
            for (int index = 0; index < systemRunData.Count; ++index)
            {
              if (systemRunData[index].DataType == item.DataType)
              {
                systemRunData[index] = item;
                item = (HardwareDataInfo.DataItem) null;
                break;
              }
            }
            if (item == null)
              return;
            systemRunData.Add(item);
          }
        }
        catch (Exception ex)
        {
        }
      }

      public HardwareDataInfo.DataItem GetSystemData(HardwareDataInfo.DataType type)
      {
        try
        {
          lock (this.systemData)
          {
            HardwareDataInfo.DataItem systemData = (HardwareDataInfo.DataItem) null;
            foreach (HardwareDataInfo.DataItem dataItem in this.systemData)
            {
              if (dataItem.DataType == type)
              {
                systemData = dataItem;
                break;
              }
            }
            return systemData;
          }
        }
        catch (Exception ex)
        {
          return (HardwareDataInfo.DataItem) null;
        }
      }

      public List<HardwareDataInfo.DataItem> GetSystemData()
      {
        try
        {
          return this.systemData;
        }
        catch
        {
          return (List<HardwareDataInfo.DataItem>) null;
        }
      }

      public HardwareDataInfo.DataItem GetSystemRunData(HardwareDataInfo.DataType type)
      {
        try
        {
          lock (this.systemRunData)
          {
            HardwareDataInfo.DataItem systemRunData = (HardwareDataInfo.DataItem) null;
            foreach (HardwareDataInfo.DataItem dataItem in this.systemRunData)
            {
              if (dataItem.DataType == type)
              {
                systemRunData = dataItem;
                break;
              }
            }
            return systemRunData;
          }
        }
        catch (Exception ex)
        {
          return (HardwareDataInfo.DataItem) null;
        }
      }

      public List<HardwareDataInfo.DataItem> GetSystemRunData()
      {
        try
        {
          return this.systemRunData;
        }
        catch
        {
          return (List<HardwareDataInfo.DataItem>) null;
        }
      }

      public void SetData(string txt)
      {
        try
        {
          JObject jobject = JObject.Parse(txt);
          if (jobject == null)
            return;
          if (jobject.ContainsKey("systemData"))
          {
            foreach (object obj in JArray.Parse(jobject["systemData"].ToString()))
            {
              HardwareDataInfo.DataItem hardwareDataItem = HardwareDataInfo.DataItem.ToHardwareDataItem(obj.ToString());
              if (hardwareDataItem != null)
                this.AddSystemData(hardwareDataItem);
            }
          }
          if (!jobject.ContainsKey("systemRunData"))
            return;
          foreach (object obj in JArray.Parse(jobject["systemRunData"].ToString()))
          {
            HardwareDataInfo.DataItem hardwareDataItem = HardwareDataInfo.DataItem.ToHardwareDataItem(obj.ToString());
            if (hardwareDataItem != null)
              this.AddSystemRunData(hardwareDataItem);
          }
        }
        catch (Exception ex)
        {
        }
      }

      public string ToSystemRunString()
      {
        JObject jobject = new JObject();
        JArray jarray = new JArray();
        foreach (HardwareDataInfo.DataItem dataItem in this.systemRunData)
          jarray.Add((JToken) dataItem.ToJson());
        jobject.Add("systemRunData", (JToken) jarray);
        return jobject.ToString(Formatting.None);
      }

      public string ToSystemDataString()
      {
        JObject jobject = new JObject();
        JArray jarray = new JArray();
        foreach (HardwareDataInfo.DataItem dataItem in this.systemData)
          jarray.Add((JToken) dataItem.ToJson());
        jobject.Add("systemData", (JToken) jarray);
        return jobject.ToString(Formatting.None);
      }

      public override string ToString()
      {
        JObject jobject = new JObject();
        JArray jarray1 = new JArray();
        foreach (HardwareDataInfo.DataItem dataItem in this.systemData)
          jarray1.Add((JToken) dataItem.ToJson());
        jobject.Add("systemData", (JToken) jarray1);
        JArray jarray2 = new JArray();
        foreach (HardwareDataInfo.DataItem dataItem in this.systemRunData)
          jarray2.Add((JToken) dataItem.ToJson());
        jobject.Add("systemRunData", (JToken) jarray2);
        return jobject.ToString(Formatting.None);
      }

      public enum DataType
      {
        SystemOS,
        BaseBoard,
        Cpu,
        Gpu,
        Ram,
        Hdd,
        Display,
        HHD,
        Fan,
        Network,
      }

      public class DataItem : IEnumerable<Dictionary<string, object>>, IEnumerable
      {
        private HardwareDataInfo.DataType _type;
        private List<Dictionary<string, object>> _data = new List<Dictionary<string, object>>();

        public int Size => this._data.Count;

        public HardwareDataInfo.DataType DataType => this._type;

        public Dictionary<string, object> this[int index] => this._data[index];

        public void Add(Dictionary<string, object> dic) => this._data.Add(dic);

        public void Remove(Dictionary<string, object> dic) => this._data.Remove(dic);

        public void RemoveAt(int index)
        {
          if (this._data.Count <= index)
            return;
          this._data.RemoveAt(index);
        }

        public void Clear()
        {
          if (this._data.Count <= 0)
            return;
          this._data.Clear();
        }

        public DataItem(HardwareDataInfo.DataType dataType) => this._type = dataType;

        public DataItem(HardwareDataInfo.DataType dataType, List<Dictionary<string, object>> data)
        {
          this._type = dataType;
          this._data = data;
        }

        public static HardwareDataInfo.DataItem ToHardwareDataItem(string txt)
        {
          try
          {
            JObject jobject = JObject.Parse(txt);
            HardwareDataInfo.DataType result;
            if (jobject == null || !jobject.ContainsKey("type") || !jobject.ContainsKey("data") || !Enum.TryParse<HardwareDataInfo.DataType>(jobject["type"].ToString(), out result))
              return (HardwareDataInfo.DataItem) null;
            List<Dictionary<string, object>> data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jobject["data"].ToString());
            return new HardwareDataInfo.DataItem(result, data);
          }
          catch (Exception ex)
          {
            return (HardwareDataInfo.DataItem) null;
          }
        }

        public override string ToString()
        {
          return $"{{\"type\":\"{this._type.ToString()}\"," + string.Format("{{\"data\":\"{1}\"}}}}", (object) JsonConvert.SerializeObject((object) this._data));
        }

        public JObject ToJson()
        {
          return new JObject()
          {
            {
              "type",
              (JToken) this._type.ToString()
            },
            {
              "data",
              (JToken) JsonConvert.SerializeObject((object) this._data)
            }
          };
        }

        public IEnumerator<Dictionary<string, object>> GetEnumerator()
        {
          return (IEnumerator<Dictionary<string, object>>) this._data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
      }
    }
}

using Application.Common.Models.ProtobufNetContracts;
using Domain.Common;
using Newtonsoft.Json;
using ProtoBuf;
using System.Text;

namespace Application.Converting
{
	public class ProtoBufToJson : IConversionMethod
	{
		public byte[] ConvertFormat(string str)
		{
			IList<Person> people;
			using (var fileStream = File.OpenRead(str))
			{
				try
				{
					people = Serializer.Deserialize<IList<Person>>(fileStream);
				}
				catch
				{
					throw new Exception("Formát súboru nie je protobuf bin, alebo dáta nie sú odpovedajúce požadovaným.");
				}
			}

			var deserializedJson = JsonConvert.SerializeObject(people);

			File.Delete(str);

			return Encoding.Unicode.GetBytes(deserializedJson);
		}
	}
}

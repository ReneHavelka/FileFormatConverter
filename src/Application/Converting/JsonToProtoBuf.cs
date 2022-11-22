using Application.Common.Models.ProtobufNetContracts;
using Domain.Common;
using Newtonsoft.Json;
using ProtoBuf;
using System.Text;

namespace Application.Converting
{
	public class JsonToProtoBuf : IConversionMethod
	{
		public byte[] ConvertFormat(string str)
		{
			try
			{
				JsonConvert.DeserializeObject<List<Person>>(str);
			}
			catch
			{
				throw new Exception("Formát súboru nie je JSON, alebo dáta nie sú odpovedajúce požadovaným.");
			}

			byte[] result = null;

			var deserializedObject = JsonConvert.DeserializeObject<List<Person>>(str);
			
			MemoryStream memoryStream = new MemoryStream();
			using (memoryStream = new MemoryStream())
			{
				Serializer.Serialize<IList<Person>>(memoryStream, deserializedObject);
			}

			return memoryStream.ToArray();
		}
	}
}

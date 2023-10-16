using System.Text;

public class ClassBuilder
{
	public StringBuilder Class { get; }
	public int CurrentTabLevel { get; private set; }

	public ClassBuilder(string className = "Sql", string? nameSpace = null)
	{
		Class = new StringBuilder();

		if (nameSpace != null)
		{
			Class.AppendLine($"namespace {nameSpace};");
		}

		Class.AppendLine($"public class {className}");
		Class.AppendLine("{");
		CurrentTabLevel++;
	}

	public string Tabs() => new string('\t', CurrentTabLevel);

	public ClassBuilder AddSection(string sectionName)
	{
		Class.AppendLine($"{Tabs()}public static class {sectionName}");
		Class.AppendLine($"{Tabs()}{{");
		CurrentTabLevel++;
		return this;
	}

	public ClassBuilder EndSection() 
	{
		CurrentTabLevel--;
		Class.AppendLine($"{Tabs()}}}");
		return this;
	}

	public ClassBuilder AddSql(string name, string sql)
	{
		Class.AppendLine($"{Tabs()}public static string {name} => \"{sql}\";");
		return this;
	}

	public string Build() 
	{
		Class.AppendLine("}");
		return Class.ToString();
	}
}

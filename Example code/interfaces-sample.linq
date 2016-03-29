<Query Kind="Expression" />

void Main()
{
	var talkers = new ICommunicator[]
	{
		new Cat(),
		new Dog(),
		new Computer(),
	};

	foreach (var talker in talkers)
	{
		talker.Talk();
	}

	Console.WriteLine();

	var dave = new Person();
	var hal = new Computer();

	dave.TalkWith(hal);

}

#region Sample Classes
public abstract class Animal
{

}

public class Cat : ICommunicator
{
	public void Talk()
	{
		Console.WriteLine("Meow!");
	}
}

public class Dog : ICommunicator
{
	public void Talk()
	{
		Console.WriteLine("Woof!");
	}
}


public class Person : Animal, ICommunicator
{
	public void Talk()
	{
		Console.WriteLine("Open the pod bay doors Hal.");
	}
}


public class Computer : ICommunicator
{
	public void Talk()
	{
		Console.WriteLine("I'm sorry Dave, I'm afraid I can't do that.");
	}
}
#endregion

#region static class with Extension Methods
public static class CommunicationExtensions
{
	public static void TalkWith(this ICommunicator talker, ICommunicator otherTalker)
	{
		talker.Talk();
		otherTalker.Talk();
	}
}
#endregion

#region Interfaces

public interface ICommunicator
{
	void Talk();
}
#endregion


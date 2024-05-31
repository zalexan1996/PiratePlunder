using Godot;
using Godot.Collections;

public partial class InsultService : Node
{
	public Array<string> Insults = new();

	public override void _Ready()
	{
		Insults.Add("You suck");
		Insults.Add("Someday you’ll go far. And I really hope you stay there.");
		Insults.Add("You’re the reason God created the middle finger.");
		Insults.Add("Remember that time when you lost? Yeah, that is now.");
		Insults.Add("You bring everyone so much joy! You know, when you give up. But, still.");
		Insults.Add("You’re a gray sprinkle on a rainbow cupcake.");
		Insults.Add("I hope your wife brings a date to your funeral.");
		Insults.Add("Your face makes onions cry.");
		Insults.Add("You are more disappointing than an unsalted pretzel.");
		Insults.Add("It is impossible to underestimate you.");
		Insults.Add("You’re cute. Like my dog. He also can't play this game.");
		Insults.Add("You have an entire life to be an idiot. Why be one here?");
		Insults.Add("Bye. Hope to see you never.");
		Insults.Add("You have miles to go before you reach mediocre.");
		Insults.Add("Did the mental hospital test too many drugs on you today?");
		Insults.Add("I’d rather treat my baby’s diaper rash than play another round with you.");
		Insults.Add("Somewhere out there is a tree tirelessly producing oxygen for you. You owe it an apology.");
		Insults.Add("You must have been born on a highway. That’s where most accidents happen.");
		Insults.Add("Grab a straw, because you suck.");
		Insults.Add("You’re the reason the gene pool needs a lifeguard.");
		Insults.Add("Don’t be ashamed of who you are. That’s your parent’s job.");
		Insults.Add("As a game, I don't have a therapist. But if I did, I'd tell them about you.");
		Insults.Add("You are a pizza burn on the roof of the world’s mouth.");
		Insults.Add("You’re about as useful as a screen door on a submarine.");
		Insults.Add("You are proof that evolution can go in reverse.");
		Insults.Add("I do not consider you a vulture. I consider you something a vulture would eat.");
		Insults.Add("If I throw a stick, will you leave?");
		Insults.Add("I like the way you try.");
		Insults.Add("I’m jealous of all the people who haven’t met you.");
		Insults.Add("I don’t have the patience or the crayons to explain how to play this game to you.");
		Insults.Add("I bet your parents change the subject when their friends ask about you.");
		Insults.Add("You’re a conversation starter. Not when you are around, but once you leave.");
		Insults.Add("I find the fact that you’ve lived this long both surprising and disappointing.");
		Insults.Add("You should carry a plant around with you to replace the oxygen you waste.");
		Insults.Add("Somewhere, somehow, you are robbing a village of their idiot.");
		Insults.Add("You are the reason why shampoo has instructions.");
		Insults.Add("Mister Rogers would be disappointed with you.");
		Insults.Add("Were you born this stupid or did you take lessons?");
		Insults.Add("I don’t know what your problem is, but I’m guessing it’s hard to pronounce.");
		Insults.Add("Stupidity isn’t a crime, so you’re free to go.");
		Insults.Add("The people who tolerate you on a daily basis are the real heroes.");
		Insults.Add("Please just tell me you don’t plan to home-school your kids.");
		Insults.Add("Jesus might love you, but everyone else definitely thinks you’re an idiot.");
		Insults.Add("Your only purpose in life is to become an organ donor.");
		Insults.Add("You’re about as useful as an ashtray on a motorcycle.");
	}

	public string GetInsult()
	{
		return Insults.PickRandom();
	}
}

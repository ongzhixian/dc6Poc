# Inference

Variables are strongly typed.
Variable<bool> is a boolean variable and 
Variable<double> is a double variable. 

Variables can be 
deterministic (with a single known value) or 
random (with an unknown or uncertain value). 

The Variable<T> type is deliberately used for both cases, since we shouldn’t have to care whether a variable is deterministic or random when we are using it.

To represent a coin, we can use a boolean Variable where true represents heads and false represents tails. 

As each coin is fair, it has a 50% chance of turning up heads and a 50% change of turning up tails. 
A distribution over a boolean value with some probability of being true is called a Bernoulli distribution. 

So we can simulate a coin by creating a boolean random variable from a Bernoulli distribution with a 50% probability of being true. 
In Infer.NET, we can create a random variable with this distribution using Variable.Bernoulli(0.5):


Other distributions
Bernoulli,  -- a model for the set of possible outcomes of any single experiment that asks a yes–no question
Gaussian,   -- normal distribution 
Gamma or    -- a two-parameter family of continuous probability distributions
Discrete.   --



# Reference

https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/matchup-app-infer-net

https://en.wikipedia.org/wiki/Probability_distribution

https://www.itl.nist.gov/div898/handbook/eda/section3/eda366b.htm

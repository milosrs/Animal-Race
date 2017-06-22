using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA {

    public List<Genome> generation;
    public int currentGeneration;
    public int currentGenome;
    private int totalPopulation;
    private int totalWeights;
    public int mutationRate;
    public int maxPermutation;
    private int genomeID;

    public GA() {
        generation = new List<Genome>();
    }

    public void Mutation(ref Genome input)
    {
        
    }

    public Genome GetBest()
    {
        Genome best = generation[0];
        for (int i = 1; i < generation.Count; i++)
        {
            if (best.fitness < generation[i].fitness)
                best = generation[i];
        }
        return best;
    }

    public Genome GetWorst()
    {
        Genome worst = generation[0];
        for (int i = 1; i < generation.Count; i++)
        {
            if (worst.fitness > generation[i].fitness)
                worst = generation[i];
        }
        return worst;
    }

    public List<Genome> Selection()
    {
        List<Genome> bestGenoms = Rulet();
        return bestGenoms;
    }

    public List<Genome> Rulet()
    {
        float totalFitness = 0;
        Dictionary<Genome, List<int>> fitnessPercent = new Dictionary<Genome, List<int>>();
        int numOfFields = 37;
        List<Genome> selectedGenoms = new List<Genome>();
        
        foreach (Genome g in generation)
        {
            totalFitness += g.fitness;
        }

        float tempFitness = 0;
        int tempField = 0;
        for (int i = 0; i < generation.Count; i++)
        {
            List<int> minMax = new List<int>();
            minMax.Add(tempField);

            tempFitness = generation[i].fitness/totalFitness;
            tempField += Mathf.CeilToInt(tempFitness*numOfFields);
            minMax.Add(tempField++);
            
            fitnessPercent.Add(generation[i], minMax);
        }

        int returnNum = generation.Count / 2;
        while(returnNum > 0)
        {
            int random = Mathf.CeilToInt(Randomize(0.0f, 37.0f));
            foreach (Genome g in fitnessPercent.Keys)
            {
                List<int> mm = fitnessPercent[g];
                if (random >= mm[0] && random <= mm[1] && selectedGenoms.Contains(g) == false)
                {
                    selectedGenoms.Add(g);
                    returnNum--;
                }
            }
        }

        return selectedGenoms;
    }

    public void CrossBreed(Genome parent1, Genome parent2,ref Genome baby1, ref Genome baby2)
    {
        int singlePoint = Mathf.CeilToInt(Randomize(0.0f, totalWeights));

        for (int i = 0; i < totalWeights; i++)
        {
            if (i < singlePoint)
            {
                baby1.weights.Add(parent1.weights[i]);
                baby2.weights.Add(parent2.weights[i]);
            }
            else
            {
                baby1.weights.Add(parent2.weights[i]);
                baby2.weights.Add(parent1.weights[i]);
            }
        }
    }

    public List<Genome> BreedNewGeneration()
    {
        List<Genome> newPopulation = new List<Genome>();

        List<Genome> bestGenoms = Rulet();

        Genome baby1 = new Genome();
        getBabyID(ref baby1, ref bestGenoms);
        Genome baby2 = new Genome();
        getBabyID(ref baby2, ref bestGenoms);
        Genome baby3 = new Genome();
        getBabyID(ref baby3, ref bestGenoms);
        Genome baby4 = new Genome();
        getBabyID(ref baby4, ref bestGenoms);

        return newPopulation;    
    }

    public void getBabyID(ref Genome baby, ref List<Genome> bestGenoms)
    {
        for (int i = 0; i < totalPopulation; i++)
        {
            for (int j = 0; j < bestGenoms.Count; j++)
            {
                if (i != bestGenoms[j].ID)
                {
                    baby.ID = i;
                    break;
                }
            }
            if (baby.ID != -1)
                break;
        }
    }



    public void GenerateNewGeneration(int numOfGenomes, int numOfWeights)
    {
        generation.Clear();

        currentGeneration = 1;
        currentGenome = -1;
        totalPopulation = numOfGenomes;
        totalWeights = numOfWeights;

        for (int i = 0; i < totalPopulation; i++)
        {
            Genome g = new Genome();
            g.ID = i;
            g.fitness = 0.0f;
            g.weights = new List<float>();
            for (int j = 0; j < totalWeights; j++)
            {
                g.weights.Add(Randomize(-2.0f, 2.0f));
            }
            generation.Add(g);
        }
    }

    public float Randomize(float min, float max)                     //Random tezine na pocetku
    {
        float rand = Random.Range(min, max);
        return rand;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GA {

    private List<Genome> generation;
    private int currentGeneration;
    private float mutationRate;

    private float maxPermutation;
    private int totalPopulation;
    private int totalWeights;

    public GA()
    {
        generation = new List<Genome>();        //Stvaramo novu populaciju
        mutationRate = 0.28f;                   //Rate = 0.28
        currentGeneration = 1;                  //Trenutna generacija
        maxPermutation = 0.3f;                  //Konstanta pretumbavanja tezina
    }

    /// <summary>
    /// Mutira genom
    /// </summary>
    public void Mutation(Genome input)
    {
        for(int i=0; i<input.weights.Count; i++)
        {
            if(Randomize(0.0f,1.0f) <= mutationRate)                                                        //Ako se pogodi da nas random broj bude <= konstanti mutacije
            {
                input.weights[i] += (Randomize(-0.3f, 0.3f) - Randomize(-0.3f, 0.3f)) * maxPermutation;     //Mutiraj tezine
            }
        }
    }

    /// <summary>
    /// Vraca najbolji genom iz nase liste genoma
    /// </summary>
    public Genome GetBest()
    {
        Genome best = generation[0];
        for (int i = 1; i < generation.Count; i++)
        {
            if (best.fitness <= generation[i].fitness)
                best = generation[i];
        }
        return best;
    }

    /// <summary>
    /// Vraca najgori genom iz nase liste genoma
    /// </summary>
    public Genome GetWorst()
    {
        Genome worst = generation[0];
        for (int i = 1; i < generation.Count; i++)
        {
            if (worst.fitness >= generation[i].fitness)
                worst = generation[i];
        }
        return worst;
    }

    /// <summary>
    /// Koristi rulet i vraca izabrane genome
    /// </summary>
    public List<Genome> Selection()
    {
        List<Genome> bestGenoms = Rulet();
        return bestGenoms;
    }

    public List<Genome> Rulet()
    {
        float totalFitness = 0;
        Dictionary<Genome, List<int>> fitnessPercent = new Dictionary<Genome, List<int>>();
        int numOfFields = 37;                                   //Broj polja = broj polja na pravom ruletu
        List<Genome> selectedGenoms = new List<Genome>();       //Lista genoma koju ce vratiti rulet
        
        foreach (Genome g in generation)    
        {
            totalFitness += g.fitness;                          //Izracunamo koliki je ukupni fitness u generaciji
        }

        float tempFitness = 0;
        int tempField = 0;
        for (int i = 0; i < generation.Count; i++)
        {
            List<int> minMax = new List<int>();                     //Trenutni genom uzima brojeve od MIN do MAX na tabli ruleta
            minMax.Add(tempField);                              

            tempFitness = generation[i].fitness/totalFitness;       //Procenat celog fitnesa u generaciji za genom
            tempField += Mathf.CeilToInt(tempFitness*numOfFields);  //Procenat polja na tabli ruleta koje zauzima genom
            minMax.Add(tempField++);                                //Dodaje se tempField u MAX i povecava se za 1
            
            fitnessPercent.Add(generation[i], minMax);              //Dodati genom sa min i max
        }

        int returnNum = generation.Count / 2;                       //Vracamo pola generacije
        while(returnNum > 0)                                        //Ruletska logika
        {
            int random = Mathf.CeilToInt(Randomize(0.0f, 37.0f));   //Imamo 37 polja, i biramo jedno
            foreach (Genome g in fitnessPercent.Keys)
            {
                List<int> mm = fitnessPercent[g];
                if (random >= mm[0] && random <= mm[1] && selectedGenoms.Contains(g) == false)
                {
                    selectedGenoms.Add(g);                          //Ako je genom zauzeo izabrano polje, a ne nalazi se u nasoj listi genoma, onda dodajemo.
                    returnNum--;
                }
            }
        }

        return selectedGenoms;
    }

    /// <summary>
    /// Koristi roditelje da bi vratio 2 bebe. Koristi Single point CrossBreed metodu.
    /// </summary>
    /// <param name="parent1">Roditelj 1 kog je vratio rulet</param>
    /// <param name="parent2">Roditelj 2 kog je vratio rulet</param>
    /// <param name="baby1">Prva beba koja ce biti ukrstana</param>
    /// <param name="baby2">Druga beba koja ce biti ukrstana</param>
    public void CrossBreed(Genome parent1, Genome parent2, Genome baby1, Genome baby2)
    {
        int singlePoint = Mathf.CeilToInt(Randomize(0.0f, totalWeights));
        if (parent1.fitness > parent2.fitness)
        {
            totalWeights = parent1.weights.Count;
        }
        else
        {
            totalWeights = parent2.weights.Count;
        }

        for (int i = 0; i < totalWeights-3; i++)
        {
            if (i < singlePoint)
            {
                try
                {
                    baby1.weights.Add(parent1.weights[i]);
                }
                catch
                {
                    baby1.weights.Add(Randomize(-0.3f, 0.3f));
                }
                try
                {
                    baby2.weights.Add(parent2.weights[i]);
                }
                catch
                {
                    baby2.weights.Add(Randomize(-0.3f, 0.3f));
                }
            }
            else
            {
                try
                {
                    baby1.weights.Add(parent2.weights[i]);
                }
                catch
                {
                    baby1.weights.Add(Randomize(-0.3f, 0.3f));
                }
                try
                {
                    baby2.weights.Add(parent1.weights[i]);
                }
                catch
                {
                    baby2.weights.Add(Randomize(-0.3f, 0.3f));
                }
            }
        }
    }

    /// <summary>
    /// Pravi novu generaciju genoma koja se sastoji od proslih genoma i novih, dobijenih preko metode CrossBreed
    /// </summary>
    public void BreedNewGeneration()
    {
        List<Genome> newPopulation = new List<Genome>();

        List<Genome> bestGenoms = Rulet();

        for (int i = 0; i < totalPopulation / 4; i++)
        {
            Genome baby1 = new Genome();
            baby1.fitness = 0.0f;
            baby1.weights = new List<float>();
            Genome baby2 = new Genome();
            baby2.fitness = 0.0f;
            baby2.weights = new List<float>();
            CrossBreed(bestGenoms[i], bestGenoms[i + 2], baby1, baby2);
            Mutation(baby1);
            Mutation(baby2);
            newPopulation.Add(baby1);
            newPopulation.Add(baby2);
        }

        if (newPopulation.Count < totalPopulation)
        {
            int remaining = totalPopulation - newPopulation.Count;
            if (remaining == bestGenoms.Count)
            {
                newPopulation.AddRange(bestGenoms);
            }
            else
            {
                for (int i = 0; i < remaining; i++)
                {
                    int addMe = Mathf.CeilToInt(UnityEngine.Random.Range(0, bestGenoms.Count - 1));
                    if (newPopulation.Contains(bestGenoms[addMe]))
                    {
                        newPopulation.Add(CreateNewGenome(GetBest().weights.Count));
                    }
                    else
                    {
                        newPopulation.Add(bestGenoms[addMe]);
                    }
                }
            }
        }
       
        currentGeneration++;
        if(currentGeneration == 200)
        {
            mutationRate /= 3;
        }
        this.generation = newPopulation;
    }

    /// <summary>
    /// Generise novu generaciju genoma
    /// </summary>
    public void GenerateNewGeneration(int numOfGenomes, int numOfWeights)
    {
        generation.Clear();

        currentGeneration = 1;
        totalPopulation = numOfGenomes;
        totalWeights = numOfWeights;

        for (int i = 0; i < totalPopulation; i++)
        {
            Genome g = new Genome();
            g.fitness = 0.0f;
            g.weights = new List<float>();
            for (int j = 0; j < totalWeights; j++)
            {
                g.weights.Add(Randomize(-0.3f, 0.3f));
            }
            generation.Add(g);
        }
    }

    public float Randomize(float min, float max)                     //Random tezine na pocetku
    {
        float rand = UnityEngine.Random.Range(min, max);
        return rand;
    }

    /// <summary>
    /// Kreira novi genom na osnovu broja tezina najboljeg genoma u generaciji
    /// </summary>
    public Genome CreateNewGenome(int totalWeights)
    {
        Genome genome = new Genome();
        genome.fitness = 0.0f;
        genome.weights = new List<float>();
        //resize
        for (int i = 0; i < totalWeights; i++)
        {
            genome.weights.Add(0.0f);
        }

        for (int j = 0; j < totalWeights; j++)
        {
            genome.weights[j] = Randomize(-0.3f, 0.3f) - Randomize(-0.3f, 0.3f);
        }

        return genome;
    }

    public void clearGeneration()
    {
        for(int i=0; i<generation.Count; i++)
        {
            generation[i] = null;
        }
        generation.Clear();
    }

    public void setGenomeFitness(float fitness, int index)
    {
        if(index >= generation.Count)
        {
            return;
        }
        else
        {
            generation[index].fitness = fitness;
        }
    }

    public Genome getGenomeAt(int index)
    {
        return generation[index];
    }

    public int getGeneration()
    {
        return currentGeneration;
    }

    public void Load(string nameOfFile)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(nameOfFile + ".bin",
                                  FileMode.Open,
                                  FileAccess.Read,
                                  FileShare.Read);
        GA obj = (GA)formatter.Deserialize(stream);
        stream.Close();

        this.generation = obj.generation;
        this.currentGeneration = obj.currentGeneration;
        this.mutationRate = obj.mutationRate;

        this.maxPermutation = obj.maxPermutation;
        this.totalPopulation = obj.totalPopulation;
        this.totalWeights = obj.totalWeights;
    }

    public void Save(string nameOfFile)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(nameOfFile + ".bin",
                                 FileMode.Create,
                                 FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, this);
        stream.Close();
    
    }
}

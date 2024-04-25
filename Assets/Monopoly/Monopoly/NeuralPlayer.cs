using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{

    public class NeuralPlayer : Player
    {
        public NEAT.Phenotype network;
        public NetworkAdapter adapter;

        public NeuralPlayer()
        {
            items = new List<int>();
        }

        public override EDecision DecideBuy(int index)
        {
            float[] Y = network.Propagate(adapter.pack);

            if (Y[0] > 0.5f)
            {
                return EDecision.YES;
            }

            return EDecision.NO;
        }

        public override EJailDecision DecideJail()
        {
            float[] Y = network.Propagate(adapter.pack);

            if (Y[1] < 0.333f)
            {
                return EJailDecision.CARD;
            }
            else if (Y[1] < 0.666f)
            {
                return EJailDecision.ROLL;
            }

            return EJailDecision.PAY;
        }

        public override EDecision DecideMortgage(int index)
        {
            float[] Y = network.Propagate(adapter.pack);

            if (Y[2] > 0.5f)
            {
                return EDecision.YES;
            }

            return EDecision.NO;
        }

        public override EDecision DecideAdvance(int index)
        {
            float[] Y = network.Propagate(adapter.pack);

            if (Y[3] > 0.5f)
            {
                return EDecision.YES;
            }

            return EDecision.NO;
        }

        public override EDecision DecideBuildHouse(int set)
        {
            float[] Y = network.Propagate(adapter.pack);

            float result = Y[5];
            float money = adapter.ConvertHouseValue(result);

            return EDecision.YES;
        }

        public override int DecideSellHouse(int set)
        {
            float[] Y = network.Propagate(adapter.pack);

            float result = Y[6];
            float money = adapter.ConvertHouseValue(result);

            return (int)money;
        }
    }
}

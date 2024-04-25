﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerInterface;

namespace MONOPOLY
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            items = new List<int>();
        }

        private EDecision PromptYesNoDecision()
        {
            UI.EDecision decision = UI.GetYesNoDecision();

            if (decision == UI.EDecision.YES)
                return EDecision.YES;
            else
                return EDecision.NO;
        }

        public override EDecision DecideBuy(int index)
        {
            return PromptYesNoDecision();
        }

        public override EJailDecision DecideJail()
        {
            return EJailDecision.PAY;
        }

        public override EDecision DecideMortgage(int index)
        {
            return PromptYesNoDecision();
        }

        public override EDecision DecideAdvance(int index)
        {
            return PromptYesNoDecision();
        }

        //public override int DecideBuildHouse(int set)
        //{
        //    float[] Y = network.Propagate(adapter.pack);

        //    float result = Y[5];
        //    float money = adapter.ConvertHouseValue(result);

        //    return (int)money;
        //}

        //public override int DecideSellHouse(int set)
        //{
        //    float[] Y = network.Propagate(adapter.pack);

        //    float result = Y[6];
        //    float money = adapter.ConvertHouseValue(result);

        //    return (int)money;
        //}
    }
}

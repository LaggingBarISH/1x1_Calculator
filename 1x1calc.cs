using System;
static class Prog
{
	static void Main()
	{
		//Amount of tnt used when getting the exposures.
		int StartTnt = 100;
		//Minumum - Maximum tnt y locations to be accepted at the end of flight path.
		int minheigth = 350; //0;//450;      //y 360-420 = 92-152 cannon
		int maxheigth = 430; //300;//510;
		//Minumum - Maximum tnt z distance from start of flight to end of flight path to be accepted.
		int mindistance = 900;
		int maxdistance = 3000;
		//Minumum - Maximum tnt amounts for brute forcing.
		int min = 20;
		int max = 400;
		//Maximum Gt of flight.
		int GtOfTravel = 50;
		//Offset start positions for different types of barrels. Ex: Stair, Trapdoor, Fence, Cactus...
		double[] Offsets = { 0, 0, 0 };
		//Really should use 1d - 0.49F and 1d - 0.98F but this works.
		double[] StartPos = { 0.509999990463, 0.0199999809265, 0.509999990463 };
		
		///*Regular Cubic*/ double[] VelocitiesAfter1Gt = { 56.3353283118259, 56.2961283119391, 56.3353283118259 };
		
		/*Cake Cubic*/ double[] VelocitiesAfter1Gt = { 22.2284889523058, 22.1892889524191, 91.6925831743843 };
		///*Double Cake*/ double[] VelocitiesAfter1Gt = { 67.2896143337152, 16.2734219924203, 67.2896143337152 };
		///*Triple Cake Far Aligned*/ double[] VelocitiesAfter1Gt = { 46.637112184307966, 11.266758421905747, 81.96826594682346 }; StartPos = { 0.5099999904633, 0.019999980926514, 0.5724999904633 };
		
		
		///*Single Cake LilyPad*/ double[] VelocitiesAfter1Gt = { 22.8131855263514, 4.95116733683868, 94.1044582848556 };
		///*Double Cake LilyPad Far Aligned*/ double[] VelocitiesAfter1Gt = { 68.2371725528844, 3.57942299911746, 68.2371725528844 };
		///*Double Cake LilyPad Close Aligned*/ double[] VelocitiesAfter1Gt = { 58.3596401946413, 4.04595700378935, 77.0347072469778 }; StartPos = { 0.4900000095367, 0.019999980926514, 0.5099999904633 };
		///*Triple Cake LilyPad Far Aligned*/ double[] VelocitiesAfter1Gt = { 46.96173271939709, 2.4511846355521896, 82.53881118656359 }; StartPos = { 0.5099999904633, 0.019999980926514, 0.5724999904633 };

		///*Trapdoor Flower Pot*/ double[] VelocitiesAfter1Gt = { 32.26699190980589, 1.9016699543124773, 80.78878703877652 };
		///*CocoaBean Cubic*/ double[] VelocitiesAfter1Gt = { 4.0309855377746535, 3.991785537887903, 92.20887826958672 };
		///*CocoaBean LilyPad Cubic*/ double[] VelocitiesAfter1Gt = { 4.034866241442181, 0.8434239842162492, 92.29764944693949 };
		///*CocoaBean Cake LilyPad Cubic*/ double[] VelocitiesAfter1Gt = { 16.38022436556238, 0.8294446757427307, 90.83580687724026 };
		///*CocoaBean Double Cake LilyPad*/ double[] VelocitiesAfter1Gt = { 27.83110360180335, 0.8005280238253655, 87.81193821978384 };


		//Get velocities for 0Gt triangle to do further calculations.
		//Double and float wizardry pulled straight from minecrafts code for precision with further triangles/distances. If some value types are incorrect fell free to contribute.
		double[] NormalizedVelocities = { VelocitiesAfter1Gt[0] / 0.98F, VelocitiesAfter1Gt[1] / 0.98F + 0.04F, VelocitiesAfter1Gt[2] / 0.98F };
		
		bool _1x1mode =	true;
		bool X =		!true;
		bool Y =		!true;
		bool Z = 		!true;
		bool XZ =		true;
		bool XY =		!true;
		bool YZ =		!true;
		bool XYZ =		!true;
		//Top Align=+(1-0.98F)
		//+X Align = +(1-0.49F) = +(1-0.99F)


		//(StartPos[1] - VelocitiesAfter1Gt[1]/0.98F).ToString("R") perfect for previous y level.
		//(StartPos[1] + VelocitiesAfter1Gt[1]-0.04F).ToString("R") perfect for next y level.
		for (int TntAmount = min; TntAmount <= max; TntAmount++)
		{
			double[] ExposureSpeed = { NormalizedVelocities[0] * TntAmount / StartTnt, NormalizedVelocities[1] * TntAmount / StartTnt, NormalizedVelocities[2] * TntAmount / StartTnt };
			double[] CollectivePosition = { StartPos[0], StartPos[1], StartPos[2] };
			for (int Gt = 0; Gt <= GtOfTravel; Gt++)
			{
				bool x1 = false;
				bool y1 = false;
				bool z1 = false;
				CollectivePosition[0] += ExposureSpeed[0];
				CollectivePosition[1] += ExposureSpeed[1] - 0.04F;
				CollectivePosition[2] += ExposureSpeed[2];
				ExposureSpeed[0] *= 0.98F;
				ExposureSpeed[1] = (ExposureSpeed[1] - 0.04F) * 0.98F;
				ExposureSpeed[2] *= 0.98F;
				if (CollectivePosition[1] > 512.0) break;
				if (CollectivePosition[1] < minheigth) continue;
				if (CollectivePosition[1] > maxheigth) break;
				if (CollectivePosition[2] < mindistance) continue;
				if (CollectivePosition[2] > maxdistance) break;


				if (_1x1mode)
				{
					if (Math.Abs(CollectivePosition[0]) + 0.49F < ((int)Math.Abs(CollectivePosition[0]) + 1) && Math.Abs(CollectivePosition[0]) - 0.49 > ((int)Math.Abs(CollectivePosition[0])))
					{
						x1 = true;
					}
					if ((Math.Abs(CollectivePosition[1]) + 0.98F) < ((int)Math.Abs(CollectivePosition[1]) + 1) && Math.Abs(CollectivePosition[1]) > ((int)Math.Abs(CollectivePosition[1])))
					{
						y1 = true;
					}
					if (Math.Abs(CollectivePosition[2]) + 0.49F < ((int)Math.Abs(CollectivePosition[2]) + 1) && Math.Abs(CollectivePosition[2]) - 0.49F > ((int)Math.Abs(CollectivePosition[2])))
					{
						z1 = true;
					}
					if (XYZ && x1 && y1 && z1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for XYZ using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (XZ && x1 && z1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for XZ using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (XY && x1 && y1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for XY using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (YZ && y1 && z1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for YZ using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (X && x1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for X using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (Y && y1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for Y using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
					else if (Z && z1)
					{
						Console.WriteLine("1x1's at X = " + CollectivePosition[0] + ", Y = " + CollectivePosition[1] + ", Z = " + CollectivePosition[2] + " for Z using " + TntAmount + " tnt at " + Gt + " Gt.");
					}
				}
				else
				{
					Console.WriteLine(TntAmount + " Tnt at " + Gt + " gt Locations: " + CollectivePosition[0].ToString("R") + ", " + CollectivePosition[1].ToString("R") + ", " + CollectivePosition[2].ToString("R"));
				}
			}
		}
		Console.WriteLine("\nDone.");
	}
}
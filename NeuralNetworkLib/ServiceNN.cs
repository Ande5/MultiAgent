
namespace NeuralNetworkLib
{
    public class ServiceNN
    {
        private NetworkTeacher _nnTeacher;
        private FileManager _fileManager;

        private ServiceNN() { }

        public ServiceNN(int learningIterations)
        {
            const int receptors = 3;

            const int numberOfOutputClasses = 3;
            int[] neuronByLayer = { 10, numberOfOutputClasses };

            _fileManager = new FileManager();

            _nnTeacher = new NetworkTeacher(neuronByLayer, receptors, 1, _fileManager)
            {
                Iteration = learningIterations,
                TestVectors = _fileManager.ReadVectors("inputDataTest.txt")
            };
        }

        public void Train()
        {
            _nnTeacher.TrainNet();
        }

        public double[] Handle(double[] surroundingDepths) =>
            _nnTeacher.HandleByNet(surroundingDepths);
        
    }
}

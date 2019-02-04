namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents the <see cref="ForEvolve.OperationResults.IMessage" /> severity level.
    /// </summary>
    public enum OperationMessageLevel
    {
        /// <summary>
        /// Messages that has no impact in the application flow.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Messages that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Messages that highlight when the current flow of execution is stopped due to a failure.
        /// These should indicate a failure in the current activity, not an application-wide failure.
        /// </summary>
        Error = 2,
    }
}

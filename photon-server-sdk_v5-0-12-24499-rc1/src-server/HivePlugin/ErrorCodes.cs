﻿namespace Photon.Hive.Plugin
{
    /// <summary>
    /// Internal plugin errors codes.
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// Indicates that a callback process method was not called.
        /// </summary>
        public const short MissingCallProcessing = 0;

        /// <summary>
        /// Indicates that an unhandled exception has occured.
        /// </summary>
        public const short UnhandledException = 1;

        /// <summary>
        /// Indicates that an exception has occured in an asynchronous callback.
        /// </summary>
        public const short AsyncCallbackException = 2;

        /// <summary>
        /// Indicates that preconditions of SetProperties were not met.
        /// </summary>
        public const short SetPropertiesPreconditionsFail = 3;

        /// <summary>
        /// Indicates that CAS ("Check And Swap") failed when setting updated properties.
        /// </summary>
        public const short SetPropertiesCASFail = 4;

        /// <summary>
        /// Indicates that an exception has occurred when updating properties.
        /// </summary>
        public const short SetPropertiesException = 5;

        /// <summary>
        /// all error codes specific either for chat or realtime may start from this number
        /// they should be defined in product specific plugin assembly (PhotonHivePlugin in case of realtime)
        /// </summary>
        public const short ProductSpecificErrorCodes = 1000;

        /// <summary>
        /// ErrorCodes which are defined by user
        /// </summary>
        public const short UserDefinedErrorCodes = 2000;
    }
}

using System;
namespace unit04_greed.Game.Casting
{
    // TODO: Implement the Artifact class here

    // 1) Add the class declaration. Use the following class comment. Make sure you
    //    inherit from the Actor class.

        /// <summary>
        /// <para>An item falling from the sky.</para>
        /// <para>
        /// The responsibility of an Artifact is to hold a point value.
        /// </para>
        /// </summary>
        public class Artifact : Actor
        {
            private int pointValue = 0;
        

    
        
        /// <summary>
        /// Constructs a new instance of Artifact.
        /// </summary>
            public Artifact()
            {

            }
       

    
        
        /// <summary>
        /// Gets the artifact's point value.
        /// </summary>
        /// <returns>The point value as an integer.</returns>
            public int GetpointValue()
            {
                return pointValue;
            }
        

    // 4) Create the SetMessage(string message) method. Use the following method comment.
        
        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="pointValue">The given message.</param>
            public void SetMessage(int points)
            {
                this.pointValue =  points;
            }
        }
}
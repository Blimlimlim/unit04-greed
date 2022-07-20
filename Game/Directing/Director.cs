using System;
using System.Collections.Generic;
using unit04_greed.Game.Casting;
using unit04_greed.Game.Services;


namespace unit04_greed.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private int totalPoints = 0;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> artifacts = cast.GetActors("artifacts");
            Point artifactVelocity = new Point(0, 5); 

            // Removing this line makes the message display permanent
            //banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in artifacts)
            {
                actor.SetVelocity(artifactVelocity);
                actor.MoveNext(maxX, maxY);
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    int newPoints = artifact.GetpointValue();
                    totalPoints = totalPoints + newPoints;
                    string message = "" + (totalPoints);
                    banner.SetText(message);

                    Random random = new Random();
                    int randomXCoordinate = random.Next(maxX);
                    Point newVelocity = new Point(randomXCoordinate, maxY);
                    newVelocity = newVelocity.Scale(15);
                    actor.SetVelocity(newVelocity);
                    actor.MoveNext(maxX, maxY);
                }
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}
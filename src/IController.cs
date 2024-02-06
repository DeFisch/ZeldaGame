/*
 *	Interface for IController
 */
public interface IController<T>
{
	/*
	 * Updates the controller and executes a command based off the state
	 */
	void Update();

	/*
	 * Register command to Dictionary associated with T identifier
	 */
    public void RegisterCommand(T identifier, ICommand command);
}
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import BetForm from '../../../Components/Bet/BetForm';
import { IBet } from '../../../Interfaces/IPage';

let container: HTMLDivElement | null;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => 
{
  if (container !== null)
  {
    document.body.removeChild(container);
    container = null; 
  }
});

describe(`The project new form component`, () => {
  it('can render', () =>
  {
    act(() =>
    {
      let dummyBet: IBet = { name: "", description: "", status: "", isLoaded: false, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
      ReactDOM.render(<BetForm bet={dummyBet} />, container);
    });
  });

  it('can validate the form', async () => {

    // display form.
    act(() =>
    {
      let dummyBet: IBet = { name: "", description: "", status: "Created", isLoaded: false, timeCurrent: 0, timeTotal: 0, successCriteria: "" } as IBet;
      ReactDOM.render(<BetForm bet={dummyBet} />, container);
    });

    // Try and submit form.
    let button = document.getElementById('bet-update') as HTMLButtonElement;
    expect(button).not.toBeNull();

    expect(button.innerHTML).toBe("Update");
    await act(async () =>
    {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true })); 
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback');
    expect(nameError).not.toBeNull();

    if (nameError !== null)
    {
      expect(nameError.innerHTML).toBe("Required"); 
    }
  });
});


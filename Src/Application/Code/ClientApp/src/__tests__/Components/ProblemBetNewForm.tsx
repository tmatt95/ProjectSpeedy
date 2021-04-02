import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProblemBetNewForm from '../../Components/ProblemBetNewForm';

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

describe(`The problem / bet new form component`, () => {
  it('can render', () => {
    act(() => {
      ReactDOM.render(<ProblemBetNewForm title="New Bet / Problem" description="Use this to add a new Bet / problem" buttonText="Button Text" saveAction={() => { return true;}} />, container);
    });
  })

  it('can validate the form', async () => {

    // display form.
    act(() => {
      ReactDOM.render(<ProblemBetNewForm title="New Bet / Problem" description="Use this to add a new Bet / problem" buttonText="Button Text" saveAction={() => { return true;}} />, container);
    });

    // Try and submit form.
    let button = document.getElementById('problem-bet-new-create');
    expect(button).not.toBeNull();

    if (button !== null)
    {
      expect(button.innerHTML).toBe("Button Text");
      await act(async () =>
      {
        if (button !== null)
        {
          button.dispatchEvent(new MouseEvent("click", { bubbles: true })); 
        }
      });
  
      // Check that there is an error for the name.
      let nameError = document.getElementById('validationNameFeedback');
      expect(nameError).not.toBeNull();

      if (nameError !== null)
      {
        expect(nameError.innerHTML).toBe("Required"); 
      }
    }
  });
});


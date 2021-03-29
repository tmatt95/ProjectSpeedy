import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProblemNewForm from '../../../Components/Problem/ProblemNewForm.tsx';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The project new form component`, () => {
  it('can render', () => {
    act(() => {
      ReactDOM.render(<ProblemNewForm />, container);
    });
  })

  it('can validate the form', async () => {

    // display form.
    act(() => {
      ReactDOM.render(<ProblemNewForm />, container);
    });

    // Try and submit form.
    let button = document.getElementById('problem-new-create');
    expect(button.innerHTML).toBe("Add Problem");
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback');
    expect(nameError.innerHTML).toBe("Required");
  });

  it('can submit a valid form', async () => {
    jest.useFakeTimers()

    // display form.
    act(() => {
      ReactDOM.render(<ProblemNewForm />, container);
    });

    // Fill in form fields
    let name = document.getElementsByName('name')[0];
    await act(async () => {
      var nativeInputValueSetter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, "value").set;
      nativeInputValueSetter.call(name, 'Problem Name');
      
      var ev2 = new Event('input', { bubbles: true});
      name.dispatchEvent(ev2);
    });

    // Try and submit form.
    let button = document.getElementById('problem-new-create');
    expect(button.innerHTML).toBe("Add Problem");
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
      jest.runAllTimers();
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback');
    expect(nameError.innerHTML).toBe("");
    expect(setTimeout).toHaveBeenCalledTimes(2);
  })
});


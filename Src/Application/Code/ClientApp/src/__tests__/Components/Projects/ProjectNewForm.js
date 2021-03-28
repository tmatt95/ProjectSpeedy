import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import * as bootstrap from 'bootstrap';
import ProjectNewForm from '../../../Components/Projects/ProjectNewForm.tsx';

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
      ReactDOM.render(<ProjectNewForm />, container);
    });
  })

  it('can validate the form', async () => {

    // display form.
    act(() => {
      ReactDOM.render(<ProjectNewForm />, container);
    });

    // Try and submit form.
    let button = document.getElementById('project-new-create');
    expect(button.innerHTML).toBe("Add Project");
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback');
    expect(nameError.innerHTML).toBe("Required");
  });

  it('can submit a valid form', async () => {

    // display form.
    act(() => {
      ReactDOM.render(<ProjectNewForm />, container);
    });

    // Fill in form fields
    let name = document.getElementsByName('name')[0];
    name.value = "New Project Name";

    // Try and submit form.
    let button = document.getElementById('project-new-create');
    expect(button.innerHTML).toBe("Add Project");
    await act(async () => {
      button.dispatchEvent(new KeyboardEvent("click", { bubbles: true }));
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback');
    expect(nameError.innerHTML).toBe("");
  });
});


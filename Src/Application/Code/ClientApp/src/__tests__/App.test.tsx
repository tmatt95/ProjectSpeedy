import { screen } from '@testing-library/react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import App  from '../App';

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

describe(`App component`, () => {
  it('can render the app', () => {
    act(() => {
      ReactDOM.render(
        <App/>,
        container);
    });
    const linkElement = screen.getByText(/Project Speedy/i);
    expect(linkElement).toBeInTheDocument();
  });
});

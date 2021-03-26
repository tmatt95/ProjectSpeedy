import { render, screen } from '@testing-library/react';
import { act } from 'react-dom/test-utils';
import App  from '../App';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`App component`, () => {
  it('can render the app', () => {
    act(() => {
      render(
        <App/>,
        container);
    });
    const linkElement = screen.getByText(/Project Speedy/i);
    expect(linkElement).toBeInTheDocument();
  });
});

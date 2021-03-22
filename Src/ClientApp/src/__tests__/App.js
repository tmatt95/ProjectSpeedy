import { render, screen } from '@testing-library/react';
import App from './../App';

describe(`The overall application component`, () => {
  it('should render without errror', () => {
    render(<App />);
    const linkElement = screen.getByText(/Project Speedy/i);
    expect(linkElement).toBeInTheDocument();
  });
})


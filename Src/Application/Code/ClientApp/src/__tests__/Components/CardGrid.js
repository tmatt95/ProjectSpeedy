import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { CardGrid, CardItem } from '../../Components/CardGrid';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The projects component`, () => {
  it('can render', () => {
    act(() => {
      let cardData = new Array();
      let data = {
        data: cardData
      };
      ReactDOM.render(<CardGrid {...data} />, container);
    });
  })
});

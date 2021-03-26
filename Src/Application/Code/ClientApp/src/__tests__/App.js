import { Test } from './../App';

describe(`Testing unit coverage in sonar`, () => {
  it('can add up', () => {
    expect(Test(1,2)).toBe(3);
  });
})

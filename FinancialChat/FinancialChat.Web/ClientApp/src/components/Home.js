import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <div >
      <p>Logged in</p>
    </div>
  </div>
);

export default connect()(Home);

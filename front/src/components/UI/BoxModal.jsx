import React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Link } from 'react-router-dom';

import classes from './BoxModal.module.scss'

const BoxModal = (props) => {
  return (
    <Container align='center'>
      <Box
        component='form'
        sx={{
          border: '1px solid rgba(0, 0, 0, 0.5)',
          p: 5,
          maxWidth: 500,
        }}
        noValidate
        autoComplete='off'
        onSubmit={props.onSubmit}
      >
        <Typography
          variant='h4'
          component='h1'
          align={props.alignTitle}
          gutterBottom
        >
          {props.title}
        </Typography>
        {props.children}
        <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
          <Button
            variant={props.regVariant}
            onClick={props.onRegister}
            type={props.register}
            className={props.register == 'submit' ? classes.link : classes.btn}
          >
            {props.register == 'submit' ? 'Register' : <Link to='/Register' className={classes.link}>Register</Link>}
            
          </Button>
          <Button
            variant={props.logVariant}
            onClick={props.onLogin}
            type={props.login}
            className={props.login == 'submit' ? classes.link : classes.btn}
          >
            {props.login == 'submit' ? 'Login' : <Link to='/Login' className={classes.link}>Login</Link>}
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default BoxModal;

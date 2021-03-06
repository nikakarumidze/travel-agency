import { useState } from 'react';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Link, useNavigate } from 'react-router-dom';

import { AppBar, Box, Toolbar, Typography } from '@mui/material';
import useAuth from '../../hooks/useAuth';

const Header = () => {
  const { auth, setAuth } = useAuth();
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => setAnchorEl(event.currentTarget);
  const handleClose = () => setAnchorEl(null);
  const navigate = useNavigate();

  const LogSmthHandler = () => {
    if (auth.token) {
      setAuth({ token: null, refreshToken: null, userName: null });
      navigate('/Login', { replace: true });
    } else {
      navigate('/Login', { replace: true });
    }
  };

  return (
    <AppBar position='sticky' sx={{ mb: 3, py: 1, background: '#E5E5E5' }}>
      <Toolbar>
        {auth.userName && (
          <Typography color='primary'>{auth.userName}</Typography>
        )}
        <Link to='/Search'>Search</Link>
        <Box sx={{ mr: 0, ml: 'auto' }}>
          <Button
            id='basic-button'
            aria-controls={open ? 'basic-menu' : undefined}
            aria-haspopup='true'
            aria-expanded={open ? 'true' : undefined}
            onClick={handleClick}
            endIcon={
              !open ? <KeyboardArrowDownIcon /> : <KeyboardArrowUpIcon />
            }
          >
            Cabinet
          </Button>

          <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
            <MenuItem>
              <Link onClick={handleClose} to='/Profile'>
                Profile
              </Link>
            </MenuItem>
            <MenuItem>
              <Link onClick={handleClose} to='/MyGuests'>
                My guests
              </Link>
            </MenuItem>
            <MenuItem>
              <Link onClick={handleClose} to='/'>
                My bookings
              </Link>
            </MenuItem>
          </Menu>
          <Button onClick={LogSmthHandler}>
            {auth.token ? 'Logout' : 'Login'}
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Header;

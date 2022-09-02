import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Regional from './components/Regional.vue'
import Tipogasto from './components/Tipogasto.vue'
import Especifgasto from './components/Especifgasto.vue'
import Moneda from './components/Moneda.vue'
import Area from './components/Area.vue'
import Empresa from './components/Empresa.vue'
import Banco from './components/Banco.vue'
import Cuenta from './components/Cuenta.vue'
import Estado from './components/Estado.vue'
import Modopago from './components/Modopago.vue'
import Tiposolicitud from './components/Tiposolicitud.vue'
import Rol from './components/Rol.vue'
import Usuario from './components/Usuario.vue'
import Proyecto from './components/Proyecto.vue'
import Nuevasolicitud from './components/Nuevasolicitud.vue'
import Missolicitudes from './components/Missolicitudes.vue'
import Login from './components/Login.vue'
import store from './store'
import Ordendepago from './components/Ordendepago.vue'
import Poraprobar from './components/Poraprobar.vue'
import Enviadosparticipados from './components/Enviadosparticipados.vue'
import Porcargar from './components/Porcargar.vue'


Vue.use(Router)

var router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta:true,
        tesoreria: true,
        contador:true
      }
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta: {
        libre: true
      }
    },
    {
      path: '/nuevasolicitudes',
      name: 'nuevasolicitudes',
      component: Nuevasolicitud,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta:true,
        tesoreria: true,
        contador:true
      }
    },
    {
      path: '/regionales',
      name: 'regionales',
      component: Regional,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/tipogastos',
      name: 'tipogastos',
      component: Tipogasto,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/especifgastos',
      name: 'especifgastos',
      component: Especifgasto,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/estados',
      name: 'estados',
      component: Estado,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/modopagos',
      name: 'modopagos',
      component: Modopago,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/tiposolicitudes',
      name: 'tiposolicitudes',
      component: Tiposolicitud,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/bancos',
      name: 'bancos',
      component: Banco,
      meta: {
        administrador: true,
        creadorcuenta: true,       
        contador: true
      }
    },
    {
      path: '/cuentas',
      name: 'cuentas',
      component: Cuenta,
      meta: {
        administrador: true,       
        creadorcuenta: true,
        tesoreria: true
      }
    },
    {
      path: '/areas',
      name: 'areas',
      component: Area,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/proyectos',
      name: 'proyectos',
      component: Proyecto,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/empresas',
      name: 'empresas',
      component: Empresa,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/monedas',
      name: 'monedas',
      component: Moneda,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/roles',
      name: 'roles',
      component: Rol,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/usuarios',
      name: 'usuarios',
      component: Usuario,
      meta: {
        administrador: true,
      }
    },
    {
      path: '/missolicitudes',
      name: 'missolicitudes',
      component: Missolicitudes,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta: true,
        tesoreria: true,
        contador: true
      }
    },
    {
      path: '/poraprobar',
      name: 'poraprobar',
      component: Poraprobar,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta:true,
        tesoreria: true,
        contador:true
      }
    },
    {
      path: '/enviadosparticipados',
      name: 'enviadosparticipados',
      component: Enviadosparticipados,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta:true,
        tesoreria: true,
        contador:true
      }
    },
    {
      path: '/ordendepagos',
      name: 'ordendepagos',
      component: Ordendepago,
      meta: {
        administrador: true,
        iniciador: true,
        aprobador: true,
        creadorcuenta: true,
        tesoreria: true,
        contador: true
      }
    },
    {
      path: '/porcargar',
      name: 'porcargar',
      component: Porcargar,
      meta: {
        administrador: true,
        tesoreria: true,
      }
    }
  ]
})

router.beforeEach((to,from,next)=>{
  if(to.matched.some(record => record.meta.libre)){
    next()
  }
  else if(store.state.usuario && store.state.usuario.rol=='Administrador')
  {
    if(to.matched.some(record => record.meta.administrador)){
      next()
    }
  }
  else if(store.state.usuario && store.state.usuario.rol=='Iniciador')
  {
    if(to.matched.some(record => record.meta.iniciador)){
      next()
    }
  }
  else if(store.state.usuario && store.state.usuario.rol=='Aprobador')
  {
    if(to.matched.some(record => record.meta.aprobador)){
      next()
    }
  }
  else if(store.state.usuario && store.state.usuario.rol=='Creador cuenta')
  {
    if(to.matched.some(record => record.meta.creadorcuenta)){
      next()
    }
  }
  else if(store.state.usuario && store.state.usuario.rol=='Contador')
  {
    if(to.matched.some(record => record.meta.contador)){
      next()
    }
  }
  else if(store.state.usuario && store.state.usuario.rol=='Tesoreria')
  {
    if(to.matched.some(record => record.meta.tesoreria)){
      next()
    }
  }
  else{
    
    next({ path: '/login' })  
  }

})

export default router